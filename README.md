# 🎥 Camera Movements

A lightweight Unity prototype demonstrating smooth camera pathing and orientation along user-defined anchor points using uniform cubic B-spline interpolation.

## 📂 Scripts Included

- `Assets/Scripts/CameraMoves.cs` — Computes camera position and rotation along the spline path over time.
- `Assets/Scripts/CreateAnchorPoints.cs` — Instantiates invisible anchor point markers in the scene from a `Vector3[]` array.

## ⚙️ Technical Specs

- **Interpolation**: Uniform cubic B-spline evaluated over 4 control points per segment:
  $$P(t) = \frac{1}{6}\left[(-a + 3b - 3c + d)t^3 + (3a - 6b + 3c)t^2 + (-3a + 3c)t + (a + 4b + c)\right]$$
- **Path Modes** (`interpolation` toggle):
  - `true`: Open path with clamped endpoint indices.
  - `false`: Closed looping path with modulo index wrapping.
- **Camera Orientation**: Tangent direction derived from `transform.position` and a nearby sampled point (`Quaternion.LookRotation`).
- **Runtime Speed**: Adjustable `speed` parameter (seconds per segment) with elapsed-time rescaling on change.

## 🖥️ Requirements & Platform

- **Engine**: Unity (`2022.3.33f1`)
- **Language**: C#

## 📄 License

This project is licensed under the MIT License — see [LICENSE](LICENSE) for details.

## 🛠️ Details

The project has been created the November 1st of 2024.
