timestamps {
    node ('docker') {
        stage ('Imagizer - Checkout') {
        checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'f41db7ff-76c4-4405-9919-c177711a3045', url: 'git@github.com:lenchis001/Imagizer.git']]]) 
        }
        stage ('Imagizer - Build') {	
        // Unable to convert a build step referring to "hudson.plugins.timestamper.TimestamperBuildWrapper". Please verify and convert manually if required.		// Shell build step
            sh """ 
            docker image build -t leon1996/imagizer -f ./Imagizer/Dockerfile . 
            """		// Shell build step
        }
        stage ('Imagizer - Stop and Remove current version') {
            sh """ 
            docker container stop imagizer || true && docker container rm imagizer || true 
            """		// Shell build step
        }
        stage ('Imagizer - Launch new version'){
            sh """ 
            docker container run -d --name imagizer -p 8081:80 -e STORAGE_PATH="/storage" -e ApiKey="123" --mount type=bind,source=/usr/docker/imagizer,target=/storage --restart=unless-stopped leon1996/imagizer 
            """ 
        }
    }
}