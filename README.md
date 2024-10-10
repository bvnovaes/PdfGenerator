public static class ErrorCodes
{
    private const string PREFIX = "API-EXTRATO";

    // 2xx - Sucesso
    public const string SUCCESS = $"{PREFIX}-SUCCESS";
    public const string RESOURCE_CREATED = $"{PREFIX}-RESOURCE-CREATED";
    public const string NO_CONTENT = $"{PREFIX}-NO-CONTENT";

    // 3xx - Redirecionamento
    public const string MOVED_PERMANENTLY = $"{PREFIX}-MOVED-PERMANENTLY";
    public const string FOUND = $"{PREFIX}-FOUND";

    // 4xx - Erro do Cliente
    public const string BAD_REQUEST = $"{PREFIX}-BAD-REQUEST";
    public const string UNAUTHORIZED = $"{PREFIX}-UNAUTHORIZED";
    public const string FORBIDDEN = $"{PREFIX}-FORBIDDEN";
    public const string NOT_FOUND = $"{PREFIX}-NOT-FOUND";
    public const string METHOD_NOT_ALLOWED = $"{PREFIX}-METHOD-NOT-ALLOWED";
    public const string NOT_ACCEPTABLE = $"{PREFIX}-NOT-ACCEPTABLE";
    public const string CONFLICT = $"{PREFIX}-CONFLICT";
    public const string GONE = $"{PREFIX}-GONE";
    public const string UNPROCESSABLE_ENTITY = $"{PREFIX}-UNPROCESSABLE-ENTITY";
    public const string TOO_MANY_REQUESTS = $"{PREFIX}-TOO-MANY-REQUESTS";

    // 5xx - Erro no Servidor
    public const string INTERNAL_SERVER_ERROR = $"{PREFIX}-INTERNAL-SERVER-ERROR";
    public const string BAD_GATEWAY = $"{PREFIX}-BAD-GATEWAY";
    public const string SERVICE_UNAVAILABLE = $"{PREFIX}-SERVICE-UNAVAILABLE";
    public const string GATEWAY_TIMEOUT = $"{PREFIX}-GATEWAY-TIMEOUT";
}

public static class ErrorMessages
{
    // 2xx - Sucesso
    public const string SUCCESS = "Operação realizada com sucesso.";
    public const string RESOURCE_CREATED = "Recurso criado com sucesso.";
    public const string NO_CONTENT = "Operação concluída, sem conteúdo adicional.";

    // 3xx - Redirecionamento
    public const string MOVED_PERMANENTLY = "O recurso foi movido permanentemente para um novo local.";
    public const string FOUND = "O recurso foi encontrado em outro local temporariamente.";

    // 4xx - Erro do Cliente
    public const string BAD_REQUEST = "A solicitação contém dados inválidos ou está malformada.";
    public const string UNAUTHORIZED = "Autenticação necessária ou inválida.";
    public const string FORBIDDEN = "Você não tem permissão para acessar este recurso.";
    public const string NOT_FOUND = "O recurso solicitado não foi encontrado.";
    public const string METHOD_NOT_ALLOWED = "O método HTTP utilizado não é permitido para este recurso.";
    public const string NOT_ACCEPTABLE = "Os parâmetros ou o formato de resposta solicitados não são aceitáveis.";
    public const string CONFLICT = "Conflito ao processar a solicitação. Verifique se o recurso já existe ou está em uso.";
    public const string GONE = "O recurso solicitado não está mais disponível.";
    public const string UNPROCESSABLE_ENTITY = "Os dados fornecidos são inválidos e não puderam ser processados.";
    public const string TOO_MANY_REQUESTS = "Muitas solicitações em um curto período de tempo. Tente novamente mais tarde.";

    // 5xx - Erro no Servidor
    public const string INTERNAL_SERVER_ERROR = "Ocorreu um erro inesperado. Nossa equipe já está investigando.";
    public const string BAD_GATEWAY = "Falha na comunicação com um serviço externo.";
    public const string SERVICE_UNAVAILABLE = "O serviço está temporariamente indisponível. Tente novamente mais tarde.";
    public const string GATEWAY_TIMEOUT = "O tempo de resposta de um serviço externo foi excedido.";
}
