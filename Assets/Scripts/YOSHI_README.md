# Yoshi's Scripts — วิธีเชื่อมใน Scene

## Script ทั้งหมด
- `ThinkingRate.cs` — ควบคุม slow-mo และ gauge
- `EnemyDetection.cs` — detect ศัตรูเข้า/ออกรัศมี
- `SlowMoEffect.cs` — vignette effect ตอน slow-mo
- `SlowMoSound.cs` — เสียง whoosh และ tick ตอน slow-mo
- `PlayerAnimationController.cs` — trigger animation และ effect ของ player
- `HitStop.cs` — หยุดเกมชั่วคราวตอน impact
- `CameraShake.cs` — กล้องสั่นตอน impact

---

## วิธีติดตั้งใน Scene

### 1. สร้าง Empty GameObject ตั้งชื่อ `ThinkingRateManager`
แปะ script เหล่านี้:
- `ThinkingRate`
- `EnemyDetection`
- `SlowMoEffect`
- `SlowMoSound`

### 2. เชื่อม Inspector ของ `ThinkingRateManager`

| ช่อง | ลากอะไรมาวาง |
|---|---|
| EnemyDetection → Enemy | GameObject ของศัตรู |
| EnemyDetection → Thinking Rate | ThinkingRateManager ตัวเอง |
| ThinkingRate → Slow Mo Effect | ThinkingRateManager ตัวเอง |
| ThinkingRate → Slow Mo Sound | ThinkingRateManager ตัวเอง |
| ThinkingRate → Thinking Bar | Slider UI ของ ThinkingBar |
| SlowMoEffect → Global Volume | Global Volume ใน Scene |
| SlowMoSound → Whoosh Clip | Sounds/Slowdown.wav |
| SlowMoSound → Whoosh Reverse Clip | Sounds/Slowdown Reverse.wav |
| SlowMoSound → Tick Clip | Sounds/Clock Ticking.wav |
| SlowMoSound → Enemy | GameObject ของศัตรู |

### 3. แปะ `PlayerAnimationController` ที่ Player model
- Animator Controller ต้องใช้ `PlayerAnimator` (อยู่ใน Animations/)
- Trigger ที่ใช้: `DoAttack`, `DoBlock`, `DoDodge`

### 4. เชื่อม Inspector ของ Player model

| ช่อง | ลากอะไรมาวาง |
|---|---|
| PlayerAnimationController → Punch Effect | PunchEffect (Particle อยู่ที่มือซ้าย) |
| PlayerAnimationController → Guard Effect | GuardEffect (Particle อยู่ที่อก) |
| PlayerAnimationController → Grab Effect | GrabEffect (Particle อยู่ที่ root) |
| PlayerAnimationController → Slam Effect | SlamEffect (Particle อยู่ที่ root) |
| PlayerAnimationController → Hit Stop | HitStop component บน Player |
| PlayerAnimationController → Camera Shake | Main Camera |

### 5. แปะ `HitStop` ที่ Player model

| ช่อง | ลากอะไรมาวาง |
|---|---|
| HitStop → Camera Shake | Main Camera |

### 6. แปะ `CameraShake` ที่ Main Camera

---

## Animation Events ที่ต้องปักธงใน Animator

| Clip | จังหวะ | Function |
|---|---|---|
| punch_action.anim | ตอนหมัดสุดแขน | `SpawnPunchEffect` |
| punch_action.anim | ตอนหมัดสุดแขน (หลัง SpawnPunchEffect) | `TriggerHitStopEvent` |
| guard_action.anim | ตอนป้องกันสุด | `SpawnGuardEffect` |
| throw_action.anim | ตอนจับตัวศัตรู | `SpawnGrabEffect` |
| throw_action.anim | ตอนกระทบพื้น | `SpawnSlamEffect` |

---

## วิธีเรียกใช้จาก Card System

```csharp
// หา component จาก Player object
PlayerAnimationController playerAnim = player.GetComponent<PlayerAnimationController>();

// เรียกตาม action การ์ด
playerAnim.PlayAttack();
playerAnim.PlayBlock();
playerAnim.PlayDodge();
```

---

## UI — Thinking Bar

- สร้าง **Slider** ใน Canvas
- **Min Value** → `0`, **Max Value** → `100`
- **Interactable** → ❌ ปิด
- **Background** → สีฟ้า (ยังไม่เต็ม)
- **Fill** → สีครีม (ค่อยๆ ทับตาม gauge)
- ลาก Slider ใส่ช่อง **Thinking Bar** ใน `ThinkingRate` component

---

## ปรับค่าได้ใน Inspector

| ค่า | อยู่ที่ | ความหมาย |
|---|---|---|
| Slow Scale | ThinkingRate | ความช้าของ slow-mo (0.25 = ช้า 4x) |
| Recharge Rate | ThinkingRate | gauge เพิ่มเร็วแค่ไหน |
| Detection Radius | EnemyDetection | รัศมี detect ศัตรู |
| Vignette Intensity On | SlowMoEffect | ความมืดขอบจอตอน slow-mo |
| Fade Out Duration | SlowMoEffect | เวลา fade ออก (วินาที) |
| Stop Duration | HitStop | เวลาหยุดเกม (วินาที) |
| Shake Duration | CameraShake | เวลากล้องสั่น (วินาที) |
| Shake Magnitude | CameraShake | ความแรงการสั่น |