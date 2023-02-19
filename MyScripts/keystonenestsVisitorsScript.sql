

CREATE TABLE public.visitors
(
    id integer NOT NULL ,
    userid integer NOT NULL,
    tenantid integer NOT NULL,
    dateofvisit date NOT NULL,
    timein time without time zone NOT NULL,
    timeout time without time zone,
    CONSTRAINT visitors_pkey PRIMARY KEY (id),
    CONSTRAINT fk_visitortenantid FOREIGN KEY (tenantid)
        REFERENCES public.tenants (id),
    CONSTRAINT fk_visitoruserid FOREIGN KEY (userid)
        REFERENCES public.users (id)
)