#!/usr/bin/env bash

# Base URL of the running API. Override for a local run, for example:
#   BASE_URL=http://localhost:5119 ./scripts/list-employees.sh
BASE_URL="${BASE_URL:-http://localhost:8080}"
API_URL="$BASE_URL/api/v1"
