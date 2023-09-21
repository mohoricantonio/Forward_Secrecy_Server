# Forward Secrecy Server for Email
This is my final thesis project that I worked on. It consists of a small e-mail client app and a secondary server witch provides Perfect Forward Secrecy. On the security server through the client application, clients send parts of the encryption keys that belong to a certain to the user (Diffie-Hellman method) and their duration. Client application, when sending an e-mail, requests those parts of the keys for a specified recipient, and the server returns them (if they exist). Also, the key is deleted from the server if it is timed out. This kind of security server and client application that encrypts content together ensure the future security of e-mail.

# Link to the full thesis: 
https://repozitorij.foi.unizg.hr/islandora/object/foi%3A7429
