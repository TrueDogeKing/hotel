# Dev container: Node (frontend) + .NET 10 SDK (backend) + Bun w jednym obrazie.
FROM mcr.microsoft.com/devcontainers/typescript-node:1-22-bookworm

# .NET 10 SDK z oficjalnego repozytorium Microsoftu (Debian 12 / bookworm)
# + curl/ca-certificates/unzip wymagane przez instalatory Bun i mise.
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y --no-install-recommends dotnet-sdk-10.0 curl ca-certificates unzip && \
    rm -rf /var/lib/apt/lists/*

# Bun (menedżer pakietów frontendu + runner zadań z root package.json)
# instalowany dla użytkownika node, do katalogu w jego HOME.
USER node
ENV BUN_INSTALL=/home/node/.bun
RUN curl -fsSL https://bun.sh/install | bash
# bun na PATH – dopisany na KOŃCU, by nie przesłaniać systemowego node/npm.
ENV PATH="${PATH}:${BUN_INSTALL}/bin"

# mise (task runner z mise.toml — db:up/down/reset/logs, ef:add, ef:update).
ENV MISE_DATA_DIR=/home/node/.local/share/mise
RUN curl -fsSL https://mise.run | sh
ENV PATH="${PATH}:/home/node/.local/bin"
