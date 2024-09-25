import socket
import time
# Create a client socket
server_ip = '10.9.225.80'  # Replace with the IP address of the server
port = 12345

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((server_ip, port))
choice = input("Do you want to use sample images? (Y/N): ")
client_socket.send(choice.encode())

while True:
    # Receive max_boxlist from server
    max_boxlist = client_socket.recv(1024).decode()
    max_box = []
    max_box.append(max_boxlist)
    print("Croud status:", max_box[-1])
client_socket.close()
