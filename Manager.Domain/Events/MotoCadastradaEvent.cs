namespace Manager.Domain.Events;
public record MotoCadastradaEvent(Guid Id, string Modelo, int Ano, string Placa);
