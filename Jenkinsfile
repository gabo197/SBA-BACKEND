pipeline {
    agent any
    triggers {
        githubPush()
    }
    stages {
        stage('Restore packages'){
           steps{
               sh 'dotnet restore SBA-BACKEND.sln'
            }
         }
        stage('Clean'){
           steps{
               sh 'dotnet clean SBA-BACKEND.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build SBA-BACKEND.sln --configuration Release --no-restore'
            }
         }
        stage('Test: Unit Test'){
           steps {
                sh 'dotnet test SBA-BACKEND.Test/SBA-BACKEND.Test.csproj --configuration Release --no-restore'
             }
          }
        stage('Publish'){
             steps{
               sh 'dotnet publish SBA-BACKEND/SBA-BACKEND.csproj --configuration Release --no-restore'
             }
        }
        /*stage('Deploy'){
             steps{
               bat '''for pid in $(lsof -t -i:9090); do
                       kill -9 $pid
               done'''
               bat 'cd WebApplication/bin/Release/netcoreapp3.1/publish/'
               bat 'nohup dotnet WebApplication.dll --urls="http://104.128.91.189:9090" --ip="104.128.91.189" --port=9090 --no-restore > /dev/null 2>&1 &'
             }
        }*/
    }
}