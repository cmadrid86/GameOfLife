Write-Host "starting up local dynamodb"
docker-compose up -d
Write-Host "sleeping 10 sec"
sleep 10

$DYNAMODB_ENDPOINT = "http://localhost:9000"
$GAME_TABLE = "game_of_life"

function create_datastore {
  Write-Host "table delete:"
  aws dynamodb delete-table --endpoint-url $DYNAMODB_ENDPOINT --table-name $GAME_TABLE --output yaml --no-cli-pager --profile local

  Write-Host "checking if table exists: ${GAME_TABLE}"
  aws dynamodb describe-table --endpoint-url $DYNAMODB_ENDPOINT --table $GAME_TABLE --output yaml --no-cli-pager --profile local

  if ($LASTEXITCODE -eq 254) {
    Write-Host "creating table definition: ${GAME_TABLE}"
    aws dynamodb create-table --endpoint-url $DYNAMODB_ENDPOINT --table-name $GAME_TABLE --no-cli-pager --profile local `
      --attribute-definitions `
        AttributeName=Id,AttributeType=S `
      --key-schema `
          AttributeName=Id,KeyType=HASH `
      --provisioned-throughput ReadCapacityUnits=10,WriteCapacityUnits=5 `
  }
  aws dynamodb describe-table --endpoint-url $DYNAMODB_ENDPOINT --table $GAME_TABLE --output yaml --no-cli-pager --profile local
}

create_datastore