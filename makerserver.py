import socket
import random

# Create a TCP/IP socket
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the address and port
server_address = ('0.0.0.0', 12345)
server_socket.bind(server_address)

# Listen for incoming connections
server_socket.listen(1)

print("Server is running...")

while True:
    # Wait for a connection
    connection, client_address = server_socket.accept()

    try:
        print("Connection from", client_address)

        # Generate a random number between 1 and 30
        random_number = random.randint(1, 30)
        print("Sending random number:", random_number)

        # Send the random number as a string
        connection.sendall(str(random_number).encode())

    finally:
        # Clean up the connection
        connection.close()
