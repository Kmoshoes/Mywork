
CREATE TABLE IF NOT EXISTS public.complains
(
    id integer NOT NULL ,
    tenantsid integer NOT NULL,
    description character varying(260) COLLATE pg_catalog."default" NOT NULL,
    attended bit varying(1) NOT NULL,
    CONSTRAINT complains_pkey PRIMARY KEY (id),
    CONSTRAINT fk_tenantscomplains FOREIGN KEY (tenantsid)
        REFERENCES public.tenants (id))