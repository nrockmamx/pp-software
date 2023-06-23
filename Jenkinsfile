def commitSha1() {
  sh 'git rev-parse HEAD > commitSha1'
  def commit = readFile('commitSha1').trim()
  sh 'rm commitSha1'
  commit.substring(0, 7)
} /* commitSha1 function get commitsha and trim to 7 digits */

node('jenkins-slave') {
  stage('Clone repository') {
    checkout scm
    env.COMMITSHA = commitSha1()
    echo 'Clone repository success'
  } /* end of stage clone repoistory */

  env.DOCKER_REGISTRY = "672492626033.dkr.ecr.ap-southeast-1.amazonaws.com"
  env.DOCKER_REPO_NAME = "newry-nonprod"
  env.DOCKER_REPO_NAME_PROD = "newry-prod"
  env.IMAGE_NAME_PROD = "${env.DOCKER_REGISTRY}/${env.DOCKER_REPO_NAME_PROD}"
  env.COMMITSHA = commitSha1()
  env.TAG_PREFIX = env.BRANCH_NAME

  env.PROJECT_NAME_SERVICE = "pp-software"
  env.IMAGE_NAME_SERVICE = "${env.DOCKER_REGISTRY}/${env.DOCKER_REPO_NAME}"
  env.IMAGE_TAG_SERVICE = "${env.PROJECT_NAME_SERVICE}-${env.TAG_PREFIX}-${env.COMMITSHA}"
  env.IMAGE_TAG_SERVICE_LATEST = "${env.PROJECT_NAME_SERVICE}-${env.TAG_PREFIX}-latest"

  if(env.BRANCH_NAME == "develop") {

    stage('Build and Push Docker image pp-software') {
      echo "Start building Docker image"
      def img = docker.build("${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}", "-f ./Dockerfile.Api .")
      docker.withRegistry("https://${env.DOCKER_REGISTRY}", "ecr:ap-southeast-1:awscredentials-newry") {
      echo "Start pushing Docker image"
      img.push()
      img.push("${env.IMAGE_TAG_SERVICE_LATEST}")
      echo "Removing docker image ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}"
      sh "docker rmi ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}"
      echo "Removing docker image ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE_LATEST}"
      sh "docker rmi ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE_LATEST}"
      }
    } /* end of stage build and push image */

    stage('Deploy pp-software') {
      withAWS(credentials: 'awscredentials-newry', region: 'ap-southeast-1') {
        sh 'aws iam get-user'
        echo 'authen with aws credentials successfully'
        withKubeConfig(credentialsId: 'kubeconfig-newry-nonprod') {
          sh "helm upgrade -i ${env.PROJECT_NAME_SERVICE} --namespace ${env.BRANCH_NAME} --wait -f deployment.yaml --set-string image.tag=${env.IMAGE_TAG_SERVICE} --set replicaCount=1 ./helm-dynamic-deployment"
        }
      }
    } /* end stage deploy */

  } /* end of develop branch */

  if(env.BRANCH_NAME == "demo") {

    stage('Build and Push Docker image pp-software') {
      echo "Start building Docker image"
      def img = docker.build("${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}", "-f ./Dockerfile.Api .")
      docker.withRegistry("https://${env.DOCKER_REGISTRY}", "ecr:ap-southeast-1:awscredentials-newry") {
      echo "Start pushing Docker image"
      img.push()
      img.push("${env.IMAGE_TAG_SERVICE_LATEST}")
      echo "Removing docker image ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}"
      sh "docker rmi ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE}"
      echo "Removing docker image ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE_LATEST}"
      sh "docker rmi ${env.IMAGE_NAME_SERVICE}:${env.IMAGE_TAG_SERVICE_LATEST}"
      }
    } /* end of stage build and push image */

    stage('Deploy pp-software') {
      withAWS(credentials: 'awscredentials-newry', region: 'ap-southeast-1') {
        sh 'aws iam get-user'
        echo 'authen with aws credentials successfully'
        withKubeConfig(credentialsId: 'kubeconfig-newry-demo') {
          sh "helm upgrade -i ${env.PROJECT_NAME_SERVICE} --namespace ${env.BRANCH_NAME} --wait -f deployment.yaml --set-string image.tag=${env.IMAGE_TAG_SERVICE}  --set replicaCount=1 ./helm-dynamic-deployment"
        }
      }
    } /* end stage deploy */

  } /* end of demo branch */

  if(env.BRANCH_NAME == "production") {

    stage('Build and Push Docker image pp-software') {
      echo "Start building Docker image"
      def img = docker.build("${env.IMAGE_NAME_PROD}:${env.IMAGE_TAG_SERVICE}", "-f ./Dockerfile.Api .")
      docker.withRegistry("https://${env.DOCKER_REGISTRY}", "ecr:ap-southeast-1:awscredentials-newry") {
      echo "Start pushing Docker image"
      img.push()
      img.push("${env.IMAGE_TAG_SERVICE_LATEST}")
      echo "Removing docker image ${env.IMAGE_NAME_PROD}:${env.IMAGE_TAG_SERVICE}"
      sh "docker rmi ${env.IMAGE_NAME_PROD}:${env.IMAGE_TAG_SERVICE}"
      echo "Removing docker image ${env.IMAGE_NAME_PROD}:${env.IMAGE_TAG_SERVICE_LATEST}"
      sh "docker rmi ${env.IMAGE_NAME_PROD}:${env.IMAGE_TAG_SERVICE_LATEST}"
      }
    } /* end of stage build and push image */

    stage('Deploy pp-software Prod') {
      withAWS(credentials: 'awscredentials-newry', region: 'ap-southeast-1') {
        sh 'aws iam get-user'
        echo 'authen with aws credentials successfully'
        withKubeConfig(credentialsId: 'kubeconfig-newry-api-prod') {
          sh "helm upgrade -i ${env.PROJECT_NAME_SERVICE} --namespace ${env.BRANCH_NAME} --wait -f deployment.yaml --set-string image.repository=${env.IMAGE_NAME_PROD} --set-string image.tag=${env.IMAGE_TAG_SERVICE} --set replicaCount=1 ./helm-dynamic-deployment"
        }
      }
    } /* end stage deploy */

  } /* end of develop branch */

}