#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

curl -sS -X POST "$API_URL/employees" \
    -H "Content-Type: application/json" \
    -d '{
        "firstName": "Maria",
        "lastName": "Dela Cruz",
        "email": "maria.delacruz@example.com",
        "phone": "+63 917 555 0200",
        "departmentId": 1,
        "position": "Software Engineer",
        "hireDate": "2024-02-01",
        "status": "Active",
        "salary": 60000
    }'
echo
