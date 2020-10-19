using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
namespace brl
{
    class brenderscript
    {
        static void Main(string[] args)
        {
            string dirpath = @"C:\brender1.1";
            if (!System.IO.Directory.Exists(dirpath))
            {
                System.IO.Directory.CreateDirectory(dirpath);
            }
            string path1 = @"C:\brender1.1\br_tmp.bat";
            if (File.Exists(path1))
            {
                System.IO.File.Delete(path1);
            }
            using (StreamWriter sr = File.AppendText(path1))
            {
                sr.WriteLine(":startall \n" +
                    "@echo off \n" +
                    "mode 100,40 \n" +
                    "title brender v1.01 - blender2.90 \n" +
                    "cls \n" +
                    "goto title \n" +
                    "\n" +
                    ":title \n" +
                    "echo. \n" +
                    "echo    ---------------------------------- \n" +
                    "echo    Blender 2.90 CMD Render v1.01 \n" +
                    "echo. \n" +
                    "echo    written by Warped Studios in Batch \n" +
                    "echo    ---------------------------------- \n" +
                    "echo. \n" +
                    "echo    ---------------------------------- \n" +
                    "echo    Disclaimer: \n" +
                    "echo    Blender is released under the GNU \n" +
                    "echo    General Public License. \n" +
                    "echo    Copyright 2020 Blender Foundation. \n" +
                    "echo    ---------------------------------- \n" +
                    "echo. \n" +
                    "echo. \n" +
                    "echo Press ENTER to begin. \n" +
                    "pause >nul \n" +
                    "color 6 \n" +
                    "goto 1 \n" +
                    "\n" +
                    ":1 \n" +
                    "set /p file=\".blend scene directory: \" \n" +
                    "if exist %file%. (echo File addded!) else goto fileerror \n" +
                    "goto 2 \n" +
                    "\n" +
                    ":2 \n" +
                    "set /p engine=\" Cycles[1] or Eevee[2] ?: \" \n" +
                    "if %engine% equ 1 goto 21 \n" +
                    "if %engine% equ 2 goto 22 \n" +
                    "if %engine% gtr 2 goto engineerror \n" +
                    "if %engine% equ 0 goto engineerror \n" +
                    "\n" +
                    ":21 \n" +
                    "echo. \n" +
                    "echo CYCLES has been chosen. \n" +
                    "echo. \n" +
                    "set rengine=CYCLES \n" +
                    "goto 3 \n" +
                    "\n" +
                    ":22 \n" +
                    "echo. \n" +
                    "echo EEVEE has been chosen. \n" +
                    "echo. \n" +
                    "set rengine=BLENDER_EEVEE \n" +
                    "goto 3 \n" +
                    "\n" +
                    ":3 \n" +
                    "set /p outtype=\"Still frame[3] or animation[4] ?: \" \n" +
                    "if %outtype% equ 3 goto 31 \n" +
                    "if %outtype% equ 4 goto 32 \n" +
                    "if %outtype% geq 4 goto engineerror \n" +
                    "if %outtype% leq 2 goto engineerror \n" +
                    "\n" +
                    ":31 \n" +
                    "echo. \n" +
                    "echo Still frame has been chosen. \n" +
                    "echo. \n" +
                    "if %outtype% equ 3 set outtype=-f \n" +
                    "goto 4f \n" +
                    "\n" +
                    ":32 \n" +
                    "echo. \n" +
                    "echo Animation has been chosen. \n" +
                    "echo. \n" +
                    "if %outtype% equ 4 set outtype=-a \n" +
                    "goto 4asf \n" +
                    "\n" +
                    ":4f \n" +
                    "set /p sframe=\"Frame: \" \n" +
                    "goto preprender-stillframe \n" +
                    "\n" +
                    ";4asf \n" +
                    "set /p start=\"Starting frame: \" \n" +
                    "goto 4aef \n" +
                    "\n" +
                    ":4aef \n" +
                    "set /p end=\"Ending frame: \" \n" +
                    "if %end% leq %start% goto framenumerror \n" +
                    "goto preprender-animation \n" +
                    "\n" +
                    ":preprender-stillframe \n" +
                    "echo. \n" +
                    "echo. \n" +
                    "echo Job added to CMD. \n" +
                    "echo Make sure the job info below is correct. \n" +
                    "echo. \n" +
                    "echo     ----------------------------------------------------------------------------------------------- \n" +
                    "echo      JOB INFORMATION \n" +
                    "echo      Blender file = %file% \n" +
                    "echo      Render engine = %rengine% \n" +
                    "echo      Frame = %sframe% \n" +
                    "echo     ----------------------------------------------------------------------------------------------- \n" +
                    "goto blenderdir-still \n" +
                    "\n" +
                    ":blenderdir-still \n" +
                    "echo. \n" +
                    "echo Before rendering, please DON'T close then Command Prompt windows whilst the file is processing. \n" +
                    "echo This will cancel the job and delete the render information. \n" +
                    "echo It is recommended that you DON'T start another jobs whilst another one is running. \n" +
                    "echo. \n" +
                    "echo Is \"blender.exe\" (2.90) located in it's default folder?  \n" +
                    "echo. \n" +
                    "set /p blenderdir=\"Y[1] N[2]: \" \n" +
                    "if %blenderdir% equ 1 goto blenderdefault-still \n" +
                    "if %blenderdir% equ 2 goto changeblenderdir-still \n" +
                    "if %blenderdir% geq 3 goto blenderdirerror \n" +
                    "if %blenderdir% equ 0 goto blenderdirerror \n" +
                    "\n" +
                    ":changeblenderdir-still \n" +
                    "set /p blenderdir=\"New Blender directory: \" \n" +
                    "if exist %blenderdir%blender.exe. (goto blenderdir-c-exist-still) else goto changeblenderdir-error \n" +
                    "\n" +
                    ":blenderdir-c-exist-still \n" +
                    "echo. \n" +
                    "echo Directory changed. \n" +
                    "goto renderfolder-still \n" +
                    "\n" +
                    ":blenderdefault-still \n" +
                    "echo. \n" +
                    "echo Directory set to the default location. \n" +
                    "set blenderdir=C:\\Program Files\\Blender Foundation\\Blender 2.90\\ \n" +
                    "goto renderfolder-still \n" +
                    "\n" +
                    ":renderfolder-still \n" +
                    "echo. \n" +
                    "set /p rfolderstill=\"Save rendered image in ...?: \" \n" +
                    "if exist %rfolderstill%. (goto renderfolder2-still) else goto foldererror-still \n" +
                    "\n" +
                    ":renderfolder2-still \n" +
                    "echo. \n" +
                    "echo Output image will be located in: %rfolderstill% \n" +
                    "echo. \n" +
                    "goto suffix-still \n" +
                    "\n" +
                    ":suffix-still \n" +
                    "set /p suffix-still=\"Suffix: \" \n" +
                    "echo. \n" +
                    "echo Begin rendering? Press any key. \n" +
                    "pause >nul \n" +
                    "goto rendercmd-still \n" +
                    "\n" +
                    ":rendercmd-still \n" +
                    "cd %blenderdir% \n" +
                    "echo. \n" +
                    "\n" +
                    ":psembed1 \n" +
                    "echo ----------------------------------------------BLENDER---------------------------------------------- \n" +
                    "blender -b %file% -o %rfolderstill%%suffix-still% -E %rengine% -f %sframe% \n" +
                    "echo. \n" +
                    "echo --------------------------------------------------------------------------------------------------- \n" +
                    "echo. \n" +
                    "echo Job complete! \n" +
                    "echo. \n" +
                    "goto jobrestart \n" +
                    "\n" +
                    ":preprender-animation \n" +
                    "echo. \n" +
                    "echo. \n" +
                    "echo Job added! \n" +
                    "echo Make sure the job info below is correct. \n" +
                    "echo. \n" +
                    "echo     ----------------------------------------------------------------------------------------------- \n" +
                    "echo      JOB INFORMATION \n" +
                    "echo      Blender file = %file% \n" +
                    "echo      Render engine = %rengine% \n" +
                    "echo      Starting frame = %start% \n" +
                    "echo      Ending frame = %end% \n" +
                    "echo     ----------------------------------------------------------------------------------------------- \n" +
                    "goto blenderdir-ani \n" +
                    "\n" +
                    ":blenderdir-ani \n" +
                    "echo. \n" +
                    "echo Before rendering, please DON'T close Command Prompt whilst processing. \n" +
                    "echo This will cancel the job and delete the render information. \n" +
                    "echo It is recommended that you DON'T start another job whilst one is already running. \n" +
                    "echo. \n" +
                    "echo Is \"blender.exe\" (2.90) located in it's default folder?  \n" +
                    "echo  - C:\\Program Files\\Blender Foundation\\Blender 2.90\\ \n" +
                    "echo. \n" +
                    "set /p blenderdir=\"Y[1] N[2]: \" \n" +
                    "if %blenderdir% equ 1 goto blenderdefault-ani \n" +
                    "if %blenderdir% equ 2 goto changeblenderdir-ani \n" +
                    "if %blenderdir% geq	3 goto blenderdirerror \n" +
                    "if %blenderdir% equ 0 goto blenderdirerror \n" +
                    "\n" +
                    ":changeblenderdir-ani \n" +
                    "set /p blenderdir=\"New Blender directory: \" \n" +
                    "if exist %blenderdir%blender.exe. (goto blenderdir-c-exist-ani) else goto changeblenderdir-error \n" +
                    "\n" +
                    ":blenderdir-c-exist-ani \n" +
                    "echo. \n" +
                    "echo Directory changed. \n" +
                    "echo. \n" +
                    "goto renderfolder-ani \n" +
                    "\n" +
                    ":blenderdefault-ani \n" +
                    "echo. \n" +
                    "echo. \n" +
                    "echo Directory set to the default location. \n" +
                    "set blenderdir=C:\\Program Files\\Blender Foundation\\Blender 2.90\\ \n" +
                    "echo. \n" +
                    "goto renderfolder-ani \n" +
                    "\n" +
                    ":renderfolder-ani \n" +
                    "set /p rfolderani=\"Save rendered images in ...?: \" \n" +
                    "if exist %rfolderani%. (goto renderfolder2-ani) else goto foldererror-ani \n" +
                    "\n" +
                    ":renderfolder2-ani \n" +
                    "echo. \n" +
                    "echo Output images will be located in: %rfolderani% \n" +
                    "echo. \n" +
                    "goto suffix-ani \n" +
                    "\n" +
                    ":suffix-ani \n" +
                    "set /p suffix-ani=\"Suffix: \" \n" +
                    "echo. \n" +
                    "echo Begin rendering? Press any key. \n" +
                    "pause >nul \n" +
                    "goto rendercmd-ani \n" +
                    "\n" +
                    ":rendercmd-ani \n" +
                    "cd %blenderdir% \n" +
                    "echo. \n" +
                    "\n" +
                    ":psembed2 \n" +
                    "echo ----------------------------------------------BLENDER---------------------------------------------- \n" +
                    "blender -b %file% -E %rengine% -o %rfolderani%%suffix-ani% -s %start% -e %end% -a \n" +
                    "echo. \n" +
                    "echo --------------------------------------------------------------------------------------------------- \n" +
                    "echo. \n" +
                    "echo Job complete! \n" +
                    "echo. \n" +
                    "goto jobrestart \n" +
                    "\n" +
                    ":fileerror \n" +
                    "echo Error: Invalid file! Exiting program. \n" +
                    "goto restart \n" +
                    "\n" +
                    ":engineerror \n" +
                    "echo Error: Invalid number! Exixing program. \n" +
                    "goto restart \n" +
                    "\n" +
                    ":framenumerror \n" +
                    "echo Error: Ending frame is smaller than or equal to starting frame! Exiting program. \n" +
                    "goto restart \n" +
                    "\n" +
                    ":blenderdirerror \n" +
                    "echo Error: Invalid number! Exiting program. \n" +
                    "goto restart \n" +
                    "\n" +
                    ":changeblenderdir-error \n" +
                    "echo Error: Cannot find \"blender.exe\". Invalid directory! Exiting program. \n" +
                    "goto restart \n" +
                    "\n" +
                    ":foldererror-ani \n" +
                    "echo Error: Invalid folder. Please create a new folder or type in valid directory. \n" +
                    "goto renderfolder-ani \n" +
                    "\n" +
                    ":foldererror-still \n" +
                    "echo Error: Invalid folder. Please create a new folder or type in valid directory. \n" +
                    "goto renderfolder-still \n" +
                    "\n" +
                    ":jobrestart \n" +
                    "set /p jobrestart=\"Do you want to render another file ? Y[1] N[2] ?: \" \n" +
                    "if %jobrestart% equ 1 goto restarty \n" +
                    "if %jobrestart% geq 2 goto quit \n" +
                    "\n" +
                    ":restart \n" +
                    "set /p restartyn=\"Do you want to restart Y[1] N[2] ?: \" \n" +
                    "if %restartyn% equ 1 goto restarty \n" +
                    "if %restartyn% geq 2 goto quit \n" +
                    "\n" +
                    ":restarty \n" +
                    "cls \n" +
                    "goto startall \n" +
                    "\n" +
                    ":quit \n" +
                    "echo Quitting... \n" +
                    "timeout /t 3 /nobreak >nul \n" +
                    "rmdir /q /s C:\\brender1.1 \n" +
                    "exit \n" +
                    "\n" +
                    ":end \n");
                sr.Close();
                {
                    {
                        ProcessStartInfo ProcessInfo;
                        Process Process;
                        ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + (path1));
                        ProcessInfo.CreateNoWindow = true;
                        ProcessInfo.UseShellExecute = true;
                        Process = Process.Start(ProcessInfo);
                    }
                }
            }
        }
    }
}
