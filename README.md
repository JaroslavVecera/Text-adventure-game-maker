# Graphical text advanture maker

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Description of use](#description-of-use)

## General info
This project is graphical editor for simple
text-based games with situation description
and some answers to choose.

Program creates custom format files (tgb), which
then can be opened in another program and
played.
	
## Technologies
Project is created with:
* WPF
	
## Description of use
In folder ZPCS\bin\Debug can be found ZPCS.exe
which is the editor itself. Then there is a template
file which describes in simplicity tgb format.

Example.tgb and all created tgbs can be opened with
enclosed TGBuilder.exe, TGBuilder takes path to
desired tgb as parameter. Editor itself can handle
opening in TGBuilder directly if path to it is set.