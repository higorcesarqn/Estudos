FROM kartoza/postgis:12.1

COPY scripts/1-create.sql /docker-entrypoint-initdb.d/1_db.sql
COPY scripts/2-store_event.sql /docker-entrypoint-initdb.d/2_db.sql

EXPOSE 5432