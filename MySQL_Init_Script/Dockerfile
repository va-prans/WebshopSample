FROM mysql
COPY 1-init.sql /docker-entrypoint-initdb.d/
COPY 2-webshop.sql /docker-entrypoint-initdb.d/
COPY 3-triggers.sql /docker-entrypoint-initdb.d/
COPY 4-procedures.sql /docker-entrypoint-initdb.d/
COPY 5-views.sql /docker-entrypoint-initdb.d/
