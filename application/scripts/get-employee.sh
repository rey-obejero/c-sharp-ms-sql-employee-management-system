#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

if [[ $# -ne 1 ]]; then
    echo "Usage: $0 <id>" >&2
    exit 1
fi

curl -sS "$API_URL/employees/$1"
echo
