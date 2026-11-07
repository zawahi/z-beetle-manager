using System;
using System.Collections.Generic;

namespace KuwagataApp.Backend.Models
{
    public enum Gender { Unknown, Male, Female }
    public enum LifeStage { Egg, Larva, Pupa, Adult }
    public enum InsectStatus { Alive, Dead, Sold, Breeded }

    public class Beetle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty; // ユーザーごと
        public string? Name { get; set; } // 管理名や番号
        public string Species { get; set; } = string.Empty; // 種類 (例: オオクワガタ)
        public string? Locality { get; set; } // 産地 (例: 能勢)
        public string? Generation { get; set; } // 累代 (例: CBF1)
        public Gender Gender { get; set; } = Gender.Unknown; // オス・メス
        public LifeStage Stage { get; set; } = LifeStage.Larva; // 現在のステージ
        
        public DateTime? HatchDate { get; set; } // 孵化日
        public DateTime? EmergenceDate { get; set; } // 羽化日
        public decimal? Size { get; set; } // サイズ(mm)
        
        public Guid? SireId { get; set; } // 父親
        public Beetle? Sire { get; set; }
        public Guid? DamId { get; set; } // 母親
        public Beetle? Dam { get; set; }

        public InsectStatus Status { get; set; } = InsectStatus.Alive;
        public string? Notes { get; set; } // メモ

        public List<BottleRecord> BottleRecords { get; set; } = new();
    }
}
