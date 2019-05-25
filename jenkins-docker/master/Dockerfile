FROM jenkins/jenkins:latest

ADD job.groovy /var/jenkins_home/
ENV CURL_OPTIONS -sSfLk 
RUN /usr/local/bin/install-plugins.sh git mstest matrix-auth workflow-aggregator docker-workflow blueocean credentials-binding job-dsl 

ENV JENKINS_USER admin
ENV JENKINS_PASS admin

# Skip initial setup
ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false

COPY executors.groovy /usr/share/jenkins/ref/init.groovy.d/
COPY default-user.groovy /usr/share/jenkins/ref/init.groovy.d/
COPY dsl-init.groovy /usr/share/jenkins/ref/init.groovy.d/

VOLUME /var/jenkins_home