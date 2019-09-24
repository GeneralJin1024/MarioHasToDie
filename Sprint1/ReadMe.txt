CA2210: cannot solve this warning. If I sign a strong key to the project, there will be a new warning that 
	MonoGame.Framework also need signing a strong key. After I use developer console to sign
	MonoGame.Framework, there is a new error that required to declare a public key in 
	InternalVisibleTo. We didn't know which public key should we use.
CA0507: cannot solve this warning because we have set up FxCop Analyzers and we have already
	change the RunCodeAnalysis to false.
Warning "Found conflicts between different versions of the same dependent assembly":
	this warning cannot be solved because if we open the AutoGenerateBindingRedirects and run
	code analysis again, then the program will fail to run with FileNotFoundException