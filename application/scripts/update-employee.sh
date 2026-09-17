#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

if [[ $# -ne 1 ]]; then
    echo "Usage: $0 <id>" >&2
    exit 1
fi

curl -sS -o /dev/null -w "HTTP %{http_code}\n" -X PUT "$API_URL/employees/$1" \
    -H "Content-Type: application/json" \
    -d '{
        "firstName": "Maria",
        "lastName": "Dela Cruz",
        "email": "maria.delacruz@example.com",
        "phone": "+63 917 555 0200",
        "departmentId": 2,
        "position": "Senior Software Engineer",
        "hireDate": "2024-02-01",
        "status": "Active",
        "salary": 75000
    }'
