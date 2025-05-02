#!/bin/bash
set -e

OS=$(uname -s)

# Build and copy the DLL to the library directory for the .NET project.
# This is useful for local development. The build pipeline handles this on GitHub.
if (running on windows)
LIB_NAME="cedar_dotnet_ffi"
OUTPUT_DIR=../CedarDotNet/runtimes/win-x64/native
mkdir -p $OUTPUT_DIR
cargo build --release
cp target/release/$LIB_NAME.dll $OUTPUT_DIR
