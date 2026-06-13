# TrustTunnel Support

This repository includes local support for adding `TrustTunnel` nodes in `v2rayN` and generating `sing-box` outbound configuration for them.

## Scope

- Only supported with `sing-box`
- Intended for manual GUI configuration
- Uses a custom `sing-box` core with `TrustTunnel` support

## Usage

1. Use `Configuration` -> `Add [TrustTunnel]` to create a node.
2. Select `sing_box` as the core.
3. Fill in:
   - `address`
   - `port`
   - `username`
   - `password`
4. Enable `QUIC` only when the server is configured for QUIC.

## Protocol mapping

- TCP mode:
  - `quic = false`
  - default `ALPN = h2`
- QUIC mode:
  - `quic = true`
  - default `ALPN = h3`
  - optional `quic_congestion_control` is generated from the UI setting

## Notes

- `TrustTunnel` nodes are generated as `type: "trusttunnel"` in `sing-box` outbound configuration.
- QUIC mode requires a `sing-trusttunnel` build that resolves domain servers correctly before dialing QUIC.
- This change currently focuses on manual node creation in the UI. Share-link import/export for `trusttunnel://` is not included here.

## Build notes

Windows release builds can be produced with:

```bash
dotnet publish v2rayN/v2rayN.csproj \
  -c Release \
  -r win-x64 \
  -p:SelfContained=false \
  -p:PublishSingleFile=true \
  -p:EnableWindowsTargeting=true \
  -o <publish-output-directory>
```

Expected output files include:

- `v2rayN.exe`
- `e_sqlite3.dll`
- `libSkiaSharp.dll`
