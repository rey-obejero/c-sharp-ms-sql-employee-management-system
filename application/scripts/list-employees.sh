#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

# An optional first argument filters by first name, last name, or email.
curl -sS -G "$API_URL/employees" --data-urlencode "search=${1:-}"
echo
