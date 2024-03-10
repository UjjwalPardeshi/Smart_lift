import cv2
import numpy as np
from openvino.runtime import Core
from time import perf_counter
import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore

# Firebase Setup (Replace with your details)
# 1. Download service account key JSON from Firebase console
# 2. Replace 'path/to/your/serviceAccountKey.json' with the actual path
cred = credentials.Certificate('realtime-persondetection/aiot-person-detection-firebase-adminsdk-oi29i-3a4fd3fc2a.json')

# Initialize Firebase app (only once, at the beginning)
print("Initializing Firebase app...")
print(cred)
firebase_admin.initialize_app(cred, name = "AIoT-person-detectio")

# Create a Firestore client
db = firestore.Client()
print("Firebase app initialized!")

# OpenVINO setup
core = Core()
detection_model_xml = "person-detection-retail-0013.xml"  # Replace if using a different model
detection_model = core.read_model(model=detection_model_xml)
device = "CPU"  # Adjust if using NCS2
compiled_model = core.compile_model(model=detection_model, device_name=device)
input_layer = compiled_model.input(0)
output_layer = compiled_model.output(0)

# Person counting function
def count_people(frame):
    person_count = 0
    resized_image = cv2.resize(src=frame, dsize=(input_layer.shape[2], input_layer.shape[3]))
    input_data = np.expand_dims(np.transpose(resized_image, (2, 0, 1)), 0).astype(np.float32)
    request = compiled_model.create_infer_request()
    request.infer(inputs={input_layer.any_name: input_data})
    result = request.get_output_tensor(output_layer.index).data

    frame_height, frame_width = frame.shape[:2]
    for detection in result[0][0]:
        label = int(detection[1])
        conf = float(detection[2])
        if conf > 0.76:
            xmin = int(detection[3] * frame_width)
            ymin = int(detection[4] * frame_height)
            xmax = int(detection[5] * frame_width)
            ymax = int(detection[6] * frame_height)
            person_count += 1

    return person_count

# Main loop (no video display)
source = 0  # Load video source (e.g., webcam index)
cap = cv2.VideoCapture(source)

while True:
    ret, frame = cap.read()

    if not ret:
        break  # Handle end of video

    # Get person count without displaying video
    person_count = count_people(frame)

    # Send person count to Firebase
    doc_ref = db.collection('person_counts').document()
    doc_ref.set({'count': person_count})

# Release resources
cap.release()
cv2.destroyAllWindows()
