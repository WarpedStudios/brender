:startall
@echo off
goto title

:title
echo.
echo    ----------------------------------
echo    Blender 2.90 CMD Render v1.01
echo.
echo    written by Warped Studios in Batch
echo    ----------------------------------
echo.
echo    ----------------------------------
echo    Disclaimer:
echo    Blender is released under the GNU 
echo    General Public License.
echo    Copyright 2020 Blender Foundation.
echo    ----------------------------------
echo.
echo.
echo Press ENTER to begin.
pause >nul
color 6
goto 1

:1
set /p file=".blend scene directory: "
if exist %file%. (echo File addded!) else goto fileerror
goto 2

:2
set /p engine=" Cycles [1] or Eevee [2]?: "
if %engine% equ 1 goto 21
if %engine% equ 2 goto 22
if %engine% gtr 3 goto engineerror
if %engine% equ 0 goto engineerror

:21
echo.
echo CYCLES has been chosen.
echo.
set rengine=CYCLES
goto 3

:22
echo.
echo EEVEE has been chosen.
echo.
set rengine=BLENDER_EEVEE
goto 3

:3
set /p outtype="Still frame [3] or animation [4]?: "
if %outtype% equ 3 goto 31
if %outtype% equ 4 goto 32
if %outtype% geq 4 goto engineerror
if %outtype% leq 2 goto engineerror

:31
echo.
echo Still frame has been chosen.
echo.
if %outtype% equ 3 set outtype=-f
goto 4f

:32
echo.
echo Animation has been chosen.
echo.
if %outtype% equ 4 set outtype=-a
goto 4asf

:4f
set /p sframe="Frame: "
goto preprender-stillframe

:4asf
set /p start="Starting frame: "
goto 4aef

:4aef
set /p end="Ending frame: "
if %end% leq %start% goto framenumerror
goto preprender-animation

:preprender-stillframe
echo.
echo.
echo Job added to CMD.
echo Make sure the job info below is correct.
echo.
echo     ----------------------------------------------------------------------------------------------------
echo      JOB INFORMATION
echo      Blender file = %file%
echo      Render engine = %rengine%
echo      Frame = %sframe%
echo     ----------------------------------------------------------------------------------------------------
goto blenderdir-still

:blenderdir-still
echo.
echo Before rendering, please DON'T close then Command Prompt windows whilst the file is processing.
echo This will cancel the job and delete the render information.
echo It is recommended that you DON'T start another jobs whilst another one is running.
echo.
echo Is "blender.exe" (2.90) located in it's default folder? 
echo  - C:\Program Files\Blender Foundation\Blender 2.90\
echo.
set /p blenderdir="Y[1] N[2]: "
if %blenderdir% equ 1 goto blenderdefault-still
if %blenderdir% equ 2 goto changeblenderdir-still
if %blenderdir% geq	3 goto blenderdirerror
if %blenderdir% equ 0 goto blenderdirerror

:changeblenderdir-still
set /p blenderdir="New Blender directory: "
if exist %blenderdir%blender.exe. (goto blenderdir-c-exist-still) else goto changeblenderdir-error

:blenderdir-c-exist-still
echo.
echo Directory changed.
goto renderfolder-still

:blenderdefault-still
echo.
echo Directory set to the default location.
set blenderdir=C:\Program Files\Blender Foundation\Blender 2.90\
goto renderfolder-still

:renderfolder-still
echo.
set /p rfolderstill="Save rendered image in...?: "
if exist %rfolderstill%. (goto renderfolder2-still) else goto foldererror-still

:renderfolder2-still
echo.
echo Output image will be located in: %rfolderstill%
echo.
goto suffix-still

:suffix-still
set /p suffix-still="Suffix: "
echo.
echo Begin rendering? Press any key.
pause >nul
goto rendercmd-still

:rendercmd-still
cd %blenderdir%
echo.
echo ---------------------------------BLENDER---------------------------------
blender -b %file% -o %rfolderstill%%suffix-still% -E %rengine% -f %sframe%
echo.
echo -------------------------------------------------------------------------
echo.
echo Job complete!
echo.
goto jobrestart

:preprender-animation
echo.
echo.
echo Job added!
echo Make sure the job info below is correct.
echo.
echo     ----------------------------------------------------------------------------------------------------
echo      JOB INFORMATION
echo      Blender file = %file%
echo      Render engine = %rengine%
echo      Starting frame = %start%
echo      Ending frame = %end%
echo     ----------------------------------------------------------------------------------------------------
goto blenderdir-ani

:blenderdir-ani
echo.
echo Before rendering, please DON'T close Command Prompt whilst processing.
echo This will cancel the job and delete the render information.
echo It is recommended that you DON'T start another job whilst one is already running.
echo.
echo Is "blender.exe" (2.90) located in it's default folder? 
echo  - C:\Program Files\Blender Foundation\Blender 2.90\
echo.
set /p blenderdir="Y[1] N[2]: "
if %blenderdir% equ 1 goto blenderdefault-ani
if %blenderdir% equ 2 goto changeblenderdir-ani
if %blenderdir% geq	3 goto blenderdirerror
if %blenderdir% equ 0 goto blenderdirerror

:changeblenderdir-ani
set /p blenderdir="New Blender directory: "
if exist %blenderdir%blender.exe. (goto blenderdir-c-exist-ani) else goto changeblenderdir-error

:blenderdir-c-exist-ani
echo.
echo Directory changed.
echo.
goto renderfolder-ani

:blenderdefault-ani
echo.
echo Directory set to the default location.
set blenderdir=C:\Program Files\Blender Foundation\Blender 2.90\
echo.
goto renderfolder-ani

:renderfolder-ani
set /p rfolderani="Save rendered images in...?: "
if exist %rfolderani%. (goto renderfolder2-ani) else goto foldererror-ani

:renderfolder2-ani
echo.
echo Output images will be located in: %rfolderani%
echo.
goto suffix-ani

:suffix-ani
set /p suffix-ani="Suffix: "
echo.
echo Begin rendering? Press any key.
pause >nul
goto rendercmd-ani

:rendercmd-ani
cd %blenderdir%
echo.
echo ---------------------------------BLENDER---------------------------------
blender -b %file% -E %rengine% -o %rfolderani%%suffix-ani% -s %start% -e %end% -a
echo.
echo -------------------------------------------------------------------------
echo.
echo Job complete!
echo.
goto jobrestart

:fileerror
echo Error: Invalid file! Exiting program.
goto restart

:engineerror
echo Error: Invalid number! Exixing program.
goto restart

:framenumerror
echo Error: Ending frame is smaller than or equal to starting frame! Exiting program.
goto restart

:blenderdirerror
echo Error: Invalid number! Exiting program.
goto restart

:changeblenderdir-error
echo Error: Cannot find "blender.exe". Invalid directory! Exiting program.
goto restart

:foldererror-ani
echo Error: Invalid folder. Please create a new folder or type in valid directory.
goto renderfolder-ani

:foldererror-still
echo Error: Invalid folder. Please create a new folder or type in valid directory.
goto renderfolder-still

:jobrestart
set /p jobrestart="Do you want to render another file? Y[1] N[2]?: "
if %jobrestart% equ 1 goto restarty
if %jobrestart% geq 2 goto quit

:restart
set /p restartyn="Do you want to restart Y[1] N[2]?: "
if %restartyn% equ 1 goto restarty
if %restartyn% geq 2 goto end

:restarty
cls
goto startall

:quit
echo Quitting...
timeout /t 3 /nobreak >nul
exit

:end