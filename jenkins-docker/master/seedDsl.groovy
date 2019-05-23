job('commit') {
  description('Build and smoke test')

  scm {
    github('martinmosegaard/vigilant-sniffle')
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