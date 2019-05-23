node('docker') {
    stage 'Checkout'
        checkout scm  
	stage 'Build'
	stage 'Unit Test'
	stage 'Integration Test'
	stage 'Run'
        sh "docker-compose -f docker-compose.yml up --build --force-recreate abort-on-container-exit"
}