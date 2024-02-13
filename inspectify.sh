#!/bin/bash

set -e

if [ -d ".bins" ]; then
    # If the .bins directory exists, navigate into it and pull the latest changes
    (cd .bins && git pull -q)
else
    # If the .bins directory does not exist, clone the repository
    git clone -q https://github.com/team-checkr/inspectify-binaries.git .bins
fi

if [[ "$(uname)" == "Darwin" ]]; then
    # MacOS
    ./.bins/inspectify-macos "$@"
elif [[ "$(uname)" == "Linux" ]]; then
    # Linux
    ./.bins/inspectify-linux "$@"
else
    echo "Unsupported operating system"
    exit 1
fi
