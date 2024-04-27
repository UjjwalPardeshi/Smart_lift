import cv2
import numpy as np
from openvino.runtime import Core
import socket
import threading
core = Core()
box = [0]
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ('0.0.0.0', 12345)
server_socket.bind(server_address)
server_socket.listen(1)
breaker = False
boxlist = []
print("Server is running...")
detection_model_xml = "person-detection-retail-0013.xml"
detection_model = core.read_model(model=detection_model_xml)
device = "CPU"  # if you have NCS2 use "MYRIAD"
compiled_model = core.compile_model(model=detection_model, device_name=device)
input_layer = compiled_model.input(0)  # Get input layer
output_layer = compiled_model.output(0)  # get outputs layer
def inference():
    while True:
        choice = "n"
        if choice.lower() == "y":
            source = 'samplecroud.png'
            frame = cv2.imread(source)
        else:
            source = 0
            cap = cv2.VideoCapture(source)

        croud = "unknown"
        while True:
            if choice.lower() != "y":
                ret, frame = cap.read()

            resized_image = cv2.resize(src=frame, dsize=(input_layer.shape[3], input_layer.shape[2]))
            input_data = np.expand_dims(np.transpose(resized_image, (2, 0, 1)), 0).astype(np.float32)
            request = compiled_model.create_infer_request()
            request.infer(inputs={input_layer.any_name: input_data})  # Infer
            result = request.get_output_tensor(output_layer.index).data
            boxes = []  # Post-process the outputs
            frame_height, frame_width = frame.shape[:2]

            for detection in result[0][0]:
                label = int(detection[1])
                conf = float(detection[2])
                if conf > 0.76:
                    xmin = int(detection[3] * frame_width)
                    ymin = int(detection[4] * frame_height)
                    xmax = int(detection[5] * frame_width)
                    ymax = int(detection[6] * frame_height)
                    boxes.append([xmin, ymin, xmax, ymax])
                    cv2.rectangle(frame, (xmin, ymin), (xmax, ymax), (0, 255, 255), 3)
                    cv2.putText(frame, f"Person {len(boxes)}", (xmin, ymin - 5), cv2.FONT_HERSHEY_SIMPLEX, 0.7,(0, 0, 0), 2)
                    boxlist.append(len(boxes))
                    max_boxlist = max(boxlist, default=0)  # Get the maximum value of boxlist
                    if max_boxlist >= 20:
                        croud = "dense"
                    elif max_boxlist <= 19 and max_boxlist >= 10:
                        croud = "Medium"
                    elif max_boxlist <= 10:
                        croud = "Low"
                    box.append(max_boxlist)
            cv2.imshow('person detection demo', frame)
            key = cv2.waitKey(1)
            if key in {ord('q'), ord('Q'), 27}:
                if choice.lower() != "y":
                    cap.release()
                cv2.destroyAllWindows()
                break
def clearbo():
    box = [0]
clearbox = threading.Thread(target=clearbo, name="box")
infr = threading.Thread(target=inference, name="inference")
infr.start()
while True:
    connection, client_address = server_socket.accept()
    try:
        print("Connection from", client_address)
        numberofpeople = max(box)
        print(max(box))
        print("Sending number of people:", numberofpeople)
        connection.sendall(str(numberofpeople).encode())
        clearbox.start()
        clearbox.join()
    finally:
        connection.close()