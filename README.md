# Call Windows C-dll (foreign) functions in C#: A complete template
Microsoft provides detailed documentation on calling (foreign) C functions defined in Windows dll in C#. Unfortunately, online examples for C# are hard to come by. This package provides a step-by-step guide on calling C functions in C# directly.

### Build Windows Dll
To create a DLL project in Visual Studio 2022:
1.	On VS2022 start-up, choose *Create a new project*.
2.	Choose C++, and choose *Dynamic-link Library (DLL)*.
3.	Enter a name, in my case winCDynamic, for your project.
4.	Delete dllmain.cpp, framework.h, pch.h, pch.cpp from Project.
5.	Move qlcfuncs.cpp to the project folder (i.e., winCDynamic) and add to the project. Note: If you provide a header file qlcfuncs.h, it will NOT work!
6.	Set the VS2022 Compiler by opening the *Property Pages dialog* and *Configuration Properties* in the left pane:
- C/C++ -> Precompiled Headers: 
set [Precompiled Headers] to [Not Using Precompiled Headers]

Note: the default calling convention should be [__cdecl] in the VS2022 Compiler. If not, set as follows:
- C/C++ -> Advanced: 
		set [Calling Convention] to [__cdecl (/Gd)]
Now, you can build your Windows dll and copy **winCDynamic.dll** to your desired folder (in my case libc, see [1](#notes)).

### C# calls C-dll functions
To call dll functions in C#, you must pass the correct arguments (i.e., string, double, struct, or array) and return types. C array can only be returned as void*, which maps to IntPtr in C# and must be Marshal.Copy() back into the correct array (for example, double[]). C# struct has to be passed as ref to match a C struct*. Read the documentation directly in **cdllfuncs.cs** [(2. Warning about path)](#notes) for what most of you need to know.

Create a C# Console App project, c4CsTest. Move cdllfuncs.cs and c4CSharp.cs to the project dir (i.e., c4CsTest), and delete program.cs. Build the c4CsTest project and execute the Console App, you will see three successful calls, such as qlDblRetArgs(). But you will get the following error as well:

- Unhandled exception. System.EntryPointNotFoundException: Unable to find an entry point named 'qlArrayArg' in DLL 'C:\qlDev\libc\winCDynamic.dll'.

The error is caused by missing the qlArrayArg() function defined in a static library. Let’s build the static library then.

### Build Windows (Static) Lib
To create a static library project in Visual Studio 2022:
1.	On VS2022 start-up, choose *Create a new project*.
2.	Choose C++, and choose *Static Library*.
3.	Enter a name, in my case winCppStatic, for your project.
4.	Delete framework.h, pch.h, pch.cpp from Project.
5.	Move qlcpptools.h and qlcpptools.cpp to the project folder (i.e., winCppStatic).
6.	Set up the VS2022 Compiler:	set [Precompiled Headers] to [Not Using Precompiled Headers].
   
Note: The default calling convention should be [__cdecl] in the VS2022 Compiler. If not, set it as in the Build Windows Dll section.

Build your Windows static library and move **qlcpptools.h and winCppStatic.lib** to libc (see [1](#notes)).

### Re-build Windows Dll
To rebuild the dll, first set up the VS2022 Compiler:
- Linker -> Input: 
add [../libc/winCppStatic.lib] to [Additional Dependencies]

- C/C++ -> Preprocessor: 
Add [\_QL__INCLUDE_STATIC_LIB_] to [Preprocessor Definitions]

Rebuild dll and then c4CsTest. Execute C#, and you will see that qlArrayArg() is successful.

### Notes:
[1] To automatically copy files after the build, select *Configuration Properties > Build Events > Post-Build Event*, and in the *Command Line* field, enter this command (for the dll project):
> copy $(TargetPath)  ..\\libc

or for the static lib project:\
> copy $(ProjectDir) qlcpptools.h ..\\libc

> copy $(TargetPath) ..\\libc

[2] WARNING: UPDATE the const dll path  [_C_DLL_PATH] in cdllfuncs.cs before running the C# program.
