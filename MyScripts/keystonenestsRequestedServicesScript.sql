
CREATE TABLE public.requestedservices
(
    id integer NOT NULL ,
    tenantsid integer NOT NULL,
    description character varying(260) NOT NULL,
    completed bit varying(1) NOT NULL,
    CONSTRAINT requestedservices_pkey PRIMARY KEY (id),
    CONSTRAINT fk_tenantsrequested FOREIGN KEY (tenantsid)
        REFERENCES public.tenants (id) 
)
