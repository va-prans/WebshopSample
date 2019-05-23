job('webshop') {
  description('webshop CI')

  scm {
    github('va-prans/WebshopSample')
  }
 
  publishers {
    downstream('test')
  }
}

buildPipelineView('Pipeline') {
  title('webshop')
  displayedBuilds(50)
  selectedJob('commit')
  alwaysAllowManualTrigger()
  showPipelineParametersInHeaders()
  showPipelineParameters()
  showPipelineDefinitionHeader()
  refreshFrequency(60)
}

// Jenkinsfile pipeline:
pipelineJob('pipeline') {
  definition {
    cpsScm {
      scm {
        github('va-prans/WebshopSample')
      }
      scriptPath('Jenkinsfile')
    }
  }
}