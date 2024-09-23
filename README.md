# FileClient
This is the client for my file storage system. I made this system to solidify my skills in Client/Server systems in C#. I wanted to see how I could manage file transfers without using FTP.

This client can be used to Upload, Retrieve or List all files from the Server.

## Usage
Open the solution in Visual Studio and click run. Make sure the server is also running. You can download the server from https://github.com/matt-jwb/FileServer. You can then connect to the server using the connect command.

## Command list
```
# Connect to the server
connect <ip> <port>

# Disconnect from the server
disconnect

# Download a file from the server
download <file>

# Upload a file to the server
upload <file>

# List files on the server
ls

# Show all commands
help

# Exit the client
exit
```
