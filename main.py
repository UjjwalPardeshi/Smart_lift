# Imports
import cv2
import numpy as np
from openvino.runtime import Core
from flask import Flask, render_template

app = Flask(__name__)
core = Core()
boxlist = []

# Initialisation
core = Core()  # Initialize OpenVINO API
boxlist = []

# Model Loading
detection_model_xml = "person-detection-retail-0013.xml"
detection_model = core.read_model(model=detection_model_xml)
device = "CPU"  # if you have NCS2 use "MYRIAD"
compiled_model = core.compile_model(model=detection_model, device_name=device)
input_layer = compiled_model.input(0)  # Get input layer
output_layer = compiled_model.output(0)  # get outputs layer

@app.route('/')
def index():
    global boxlist  # Ensure boxlist is accessible inside the function
    max_crowd = max(boxlist) if boxlist else 0
    return render_template('index.html', max_crowd=max_crowd)

if __name__ == '__main__':
    choice = input("Do you want to use sample images? (Y/N): ")
    if choice == "Y" or choice == "y":
        source = 'samplecroud2.png'
        frame = cv2.imread(source)
    else:
        source = 0
        cap = cv2.VideoCapture(source)

    N, C, H, W = input_layer.shape

    while True:  # Main loop
        if choice != "Y" and choice != "y":
            ret, frame = cap.read()
        resized_image = cv2.resize(src=frame, dsize=(W, H))
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
                cv2.putText(frame, f"Person {len(boxes)}", (xmin, ymin - 5), cv2.FONT_HERSHEY_SIMPLEX, 0.7,
                            (0, 0, 0), 2)
                boxlist.append(len(boxes))
                print(f"{max(boxlist)}")

        cv2.imshow('person detection demo', frame)
        key = cv2.waitKey(1)
        if key in {ord('q'), ord('Q'), 27}:
            if choice != "Y" and choice != "y":
                cap.release()
            cv2.destroyAllWindows()
            break

    app.run(debug=True, port=5001)  # Change the port to 5001 or any other available port
