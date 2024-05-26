🌏[한국어](README.kr.md) | [English](README.en.md)


[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fkillkimno%2FMORT&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
[![GitHub downloads](https://img.shields.io/github/downloads/killkimno/MORT/total.svg?logo=github)](https://github.com/killkimno/MORT/releases)

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y6DIUR2)

<img src="https://github.com/killkimno/MORT/blob/main/MORT_LOGO.png" width="90%"></img>


[![Video Label](https://img.youtube.com/vi/LHTErVnsaws/0.jpg)](https://youtu.be/LHTErVnsaws)

https://youtu.be/LHTErVnsaws

Sample video

# MORT #

MORT is a program that extracts dialogs from the screen in real time using OCR and outputs a translations using DB or machine translation.

Currently, English and Japanese translation/extraction can be extracted by default, and it can also be translated by linking with a hooking program using the save to clipboard function.

[Latest version download and release notes - https://blog.naver.com/killkimno/70179867557]

## Features ##
* Realtime translate
* OCR - TesseractOCR , Windows OCR, NHOcr, Easy OCR
* Machine translation - Naver Papago, Google Web, Google Sheet, ezTrans, DeepL
* Language Patch with using DB
* Multiple OCR areas
* Img adjust

## System Requirement ##
* Windows 10 or higer
* 64bit os
* .NET 7 or higer
  - https://dotnet.microsoft.com/ko-kr/download/dotnet/thank-you/runtime-desktop-7.0.8-windows-x64-installer
* Visual Studio 2022 Visual C++ (x64) - vcredist_x64.exe
  - https://aka.ms/vs/17/release/vc_redist.x64.exe


## How to use ##
#### Basic usage ####

1. After setting in the quick settings, press Trnaselate on the remote control to start translation
2. Or Preferences tab -> Set the OCR language according to the language of the game to be translated
3. Remote control -> Click Search and select the area where the lines appear
4. Click Apply on MORT main form
5. Remote control -> Real-time translation by pressing Translate

#### User Manual ####
* https://blog.naver.com/killkimno/221904784013

## Custom usage ##
#### Add translation result language code ####
You can add Google Translator language code in the UserData/UserTransCode.txt file​

```
code, name
(ex : it, Italian)
```

​A list of language codes can be found here: https://cloud.google.com/translate/docs/languages?hl=en

#### Custom API usage ####
1. You can use a custom API based on HTTP
2. Translation Type -> Custom API
3. Advanced Settings -> Custom API URL Settings
```
POST Rule
name - string
text - string - ocr string
target - string - translation result language code
source - string - ocr language code

ex
{
"name" : "test",
"text" : "tank divsion" ,
"target" : "ko",
"source" : "en"
...
}


Reponse Rule
result - string - translation result text
errorCode - string - error code
errorMessage - string - error message

ex
{
"result" : "탱크 사단",
"errorMessage" : "",
"errorCode" : "0"
}
```

4. Example1 - Using LibreTranslate
* Translator address to use as an example
  - https://github.com/LibreTranslate/LibreTranslate
* sample code
  - https://github.com/CommitComedian/LT_Mort
  - Thank you [@CommitComedian](https://github.com/CommitComedian) for creating the sample!

  Example2 - Using NLLB
* Translator address to use as an example
  - https://github.com/thammegowda/nllb-serve
* sample code
  - https://github.com/TUVup/NLLB_serveMORTapi
  - Thank you [@TUVup](https://github.com/TUVup) for creating the sample!

## FAQ ##
<b>Can i use FullScreen Mode?</b>
- No you can't use it in fullscreen games, please use windowed mode, borderless windowed mode instead

<b>I'm using 32-bit Windows. Can I use MORT?</b>
- Use 32bit version MORT
- https://blog.naver.com/killkimno/222936631523

<b>I'm using 64-bit Windows. But Can't run with this error 0x8007045A.</b>
- CPU must Support AVX2. If Your CPU not support AVX2, Use 32bit version MORT instead
- https://blog.naver.com/killkimno/222936631523


## Develop the project
### Development environment ###

* Visaul Studio 2019 or higer
* Tesseract OCR 5.2.0 
* NHocr 0.21

### Create a build and run environment ###

Dependencies are managed using vcpkg. Note that the first build of MORT is
expected to take much longer than subsequent builds, since the vcpkg
dependencies are built from source.

vcpkg must be installed before building MORT for the first time. For complete
instructions, see the
[github page](https://github.com/microsoft/vcpkg). Brief instructions follow.

```
> git clone https://github.com/microsoft/vcpkg
> .\vcpkg\bootstrap-vcpkg.bat
> .\vcpkg\vcpkg integrate install
```

vcpkg is now installed and integrated with Visual Studio. Next, clone and build
the repository.

```
> git clone https://github.com/fmahnke/MORT.git
> cd MORT
```

The repository is ready to build with either Visual Studio or the command line
tools.

#### Building with Visual Studio

1. Set project to MORT.
2. Set configuration to x64 Release.
3. Build the solution and start the program.

#### Building with command line tools

```
> dotnet restore
> msbuild MORT.sln -t:Build -p:Configuration=Release /p:Platform=x64
```

Run the resulting `MORT\bin\**\MORT.exe` from the working directory where it is
generated.

### Related Project ###

* MORT Core - MORT_CORE_DLL
  - https://github.com/killkimno/MORT_CORE
  
* MORT NHocr - nhocr.DLL
  - https://github.com/killkimno/MORT_NHOCR

## ETC ##
### Development Plans Trello ###

- https://trello.com/b/gPa1EL5x/mort

### Discord ###
- [Discord](https://discord.com/invite/ha5yNy9) ![Discord Badge](https://discord.com/api/guilds/742743719958151298/widget.png?style=shield)
