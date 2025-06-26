@echo off

set CurDir=%CD%
cd .\Bin\Release\net9.0\
Main.exe --m Develop

cd %CurDir%
