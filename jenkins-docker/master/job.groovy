pipelineJob('webshop') { 

  def repo = 'https://github.com/va-prans/WebshopSample.git' 

  description("webshop") 
  keepDependencies(false) 

  properties{ 
	githubProjectUrl (repo) 
    rebuild { 
      autoRebuild(false) 
    } 
  } 

  definition { 
    cpsScm { 
      scm { 
        git { 
          remote { url(repo) } 
          branches('master') 
          scriptPath('Jenkinsfile') 
          extensions { }  // required as otherwise it may try to tag the repo, which you may not want 
        } 
      } 
    } 
  }
  if (!jenkins.model.Jenkins.instance.getItemByFullName("webshop")) {
  queue("webshop")
  }
}
