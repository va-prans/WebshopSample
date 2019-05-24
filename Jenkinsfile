node('docker') {
    stage 'Checkout'
        checkout scm  
	stage 'Build and Unit Test'
		sh "docker build -t webshopapi:B${BUILD_NUMBER} -f Dockerfile ."
		sh "docker build -t webshopapi:test-B${BUILD_NUMBER} -f Dockerfile-Integration ."
	stage 'Integration Test'
		sh "docker-compose -f docker-compose-integration.yml up --force-recreate --abort-on-container-exit"
		sh "docker-compose -f docker-compose-integration.yml down -v"
	stage 'Run'
        sh "docker-compose -f docker-compose.yml up --build --force-recreate -d"
}