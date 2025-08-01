#!/bin/bash
set -e

# Build and copy the DLL to the library directory for the .NET project.
# This is useful for local development. The build pipeline handles this on GitHub.
LIB_NAME="libcedar_dotnet_ffi"
OUTPUT_DIR=../CedarDotNet/runtimes/linux-x64/native
mkdir -p $OUTPUT_DIR
cargo build --release
cp target/release/$LIB_NAME.so $OUTPUT_DIR
