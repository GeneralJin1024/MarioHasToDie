CA2210: cannot solve this warning because we are not sure whether setting a strong key will
	influence the running of program.
CA0507: cannot solve this warning because we have set up FxCop Analyzers and we have already
	change the RunCodeAnalysis to false.
Warning "Found conflicts between different versions of the same dependent assembly":
	this warning cannot be solved because if we open the AutoGenerateBindingRedirects and run
	code analysis again, then the program will fail to run with FileNotFoundException