#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

echo "== health =="
curl -sS "$BASE_URL/health/ready"
echo
echo "== departments =="
"$SCRIPT_DIR/list-departments.sh"
echo "== employees =="
"$SCRIPT_DIR/list-employees.sh"

echo "== create =="
created="$("$SCRIPT_DIR/create-employee.sh")"
echo "$created"
id="$(printf '%s' "$created" | grep -o '"id":[0-9]*' | head -1 | cut -d: -f2)"
echo "new id: $id"

echo "== update =="
"$SCRIPT_DIR/update-employee.sh" "$id"

echo "== delete =="
"$SCRIPT_DIR/delete-employee.sh" "$id"
