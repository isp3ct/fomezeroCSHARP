using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fomezero.Models;

public partial class FomezeroContext : DbContext
{
    public FomezeroContext()
    {
    }

    public FomezeroContext(DbContextOptions<FomezeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doacao> Doacoes { get; set; }

    public virtual DbSet<DoacoesInstituico> DoacoesInstituicoes { get; set; }

    public virtual DbSet<Instituico> Instituicoes { get; set; }

    public virtual DbSet<LocaisRetiradum> LocaisRetirada { get; set; }

    public virtual DbSet<RetiradaDoacao> RetiradaDoacoes { get; set; }

    public virtual DbSet<TipoDoacao> TipoDoacaos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=fomezero;Username=postgres;Password=2812");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doacoes_pkey");

            entity.ToTable("doacoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataDoacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_doacao");
            entity.Property(e => e.DescricaoAlimento)
                .HasMaxLength(255)
                .HasColumnName("descricao_alimento");
            entity.Property(e => e.TipoDoacaoId).HasColumnName("tipo_doacao_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.TipoDoacao).WithMany(p => p.Doacaos)
                .HasForeignKey(d => d.TipoDoacaoId)
                .HasConstraintName("Doacoes_tipo_doacao_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Doacaos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("Doacoes_usuario_id_fkey");
        });

        modelBuilder.Entity<DoacoesInstituico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Doacoes_instituicoes_pkey");

            entity.ToTable("doacoes_instituicoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DoacaoId).HasColumnName("doacao_id");
            entity.Property(e => e.InstituicaoId).HasColumnName("instituicao_id");

            entity.HasOne(d => d.Doacao).WithMany(p => p.DoacoesInstituicos)
                .HasForeignKey(d => d.DoacaoId)
                .HasConstraintName("Doacoes_instituicoes_doacao_id_fkey");

            entity.HasOne(d => d.Instituicao).WithMany(p => p.DoacoesInstituicos)
                .HasForeignKey(d => d.InstituicaoId)
                .HasConstraintName("Doacoes_instituicoes_instituicao_id_fkey");
        });

        modelBuilder.Entity<Instituico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instituicoes_pkey");

            entity.ToTable("instituicoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contato)
                .HasMaxLength(100)
                .HasColumnName("contato");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<LocaisRetiradum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locais_retirada_pkey");

            entity.ToTable("locais_retirada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Endereco)
                .HasMaxLength(255)
                .HasColumnName("endereco");
            entity.Property(e => e.HorarioDisponivel)
                .HasMaxLength(50)
                .HasColumnName("horario_disponivel");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<RetiradaDoacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("retirada_Doacoes_pkey");

            entity.ToTable("retirada_doacoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeneficiarioId).HasColumnName("beneficiario_id");
            entity.Property(e => e.DataAgendada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_agendada");
            entity.Property(e => e.DataRetirada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_retirada");
            entity.Property(e => e.DoacaoId).HasColumnName("doacao_id");
            entity.Property(e => e.LocalRetiradaId).HasColumnName("local_retirada_id");

            entity.HasOne(d => d.Beneficiario).WithMany(p => p.RetiradaDoacaos)
                .HasForeignKey(d => d.BeneficiarioId)
                .HasConstraintName("retirada_Doacoes_beneficiario_id_fkey");

            entity.HasOne(d => d.Doacao).WithMany(p => p.RetiradaDoacaos)
                .HasForeignKey(d => d.DoacaoId)
                .HasConstraintName("retirada_Doacoes_doacao_id_fkey");

            entity.HasOne(d => d.LocalRetirada).WithMany(p => p.RetiradaDoacaos)
                .HasForeignKey(d => d.LocalRetiradaId)
                .HasConstraintName("retirada_Doacoes_local_retirada_id_fkey");
        });

        modelBuilder.Entity<TipoDoacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_doacao_pkey");

            entity.ToTable("tipo_doacao");

            entity.HasIndex(e => e.Descricao, "tipo_doacao_descricao_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_usuario_pkey");

            entity.ToTable("tipo_usuario");

            entity.HasIndex(e => e.Descricao, "tipo_usuario_descricao_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.DocumentoIdentificacao, "usuarios_documento_identificacao_key").IsUnique();

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentoIdentificacao)
                .HasMaxLength(20)
                .HasColumnName("documento_identificacao");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .HasColumnName("senha");
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .HasColumnName("telefone");
            entity.Property(e => e.TipoUsuarioId).HasColumnName("tipo_usuario_id");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_tipo_usuario_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
