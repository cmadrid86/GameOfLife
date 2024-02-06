# Requirements

* .net 7
* docker
* aws console
* local dynamodb

# Instructions

1 Create local profile for aws as [local]

[local]
aws_access_key_id = myid
aws_secret_access_key = mysecretid
region = us-west-2

2 Run setup.ps1 to configure and init the dynamodb table
3 Run the project