SEPP Client
===========

BGS OCR Client. Uploads BGS data to SEPP Central

AWS Configuration
-----------------

A aws configuration file is needed to send data to the server. 

1) Copy AppSettingsSecrets.config.example to:


C:\Secrets\AppSettingsSecrets.config

2) Fill out the approriate AWS credentials


OCR Setup
---------

If you want to use OCR functionality do the following:

1) Download and install Engu-cv 3.0.0. Currently available from here:

Sourceforge:
https://sourceforge.net/projects/emgucv/files/emgucv/3.0.0/

Sourceforge exe download:
https://sourceforge.net/projects/emgucv/files/emgucv/3.0.0/libemgucv-windows-universal-cuda-3.0.0.2158.exe/download

2) Build the project

3) Copy the bin/x64 (or bin/x86) dlls from emgu-cv to sepp-client Debug/Release x64 (or x86) folder.

For example, copy this:

C:\Emgu\emgucv-windows-universal-cuda 3.0.0.2158\bin\x64\ to SeppOcrClient\bin\Debug\x64

4) Run the app. You should no longer get missing DLL errors when performing OCR.
