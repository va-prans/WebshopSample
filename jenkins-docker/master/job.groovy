pipelineJob('webshop') {   
	def repo = 'https://github.com/va-prans/WebshopSample'
	properties{ 
		githubProjectUrl (repo) 
		rebuild { 
			autoRebuild(false) 
		} 
	}
	definition {
		cps { 			 
			scriptPath('Jenkinsfile')
		} 
	} 
}
