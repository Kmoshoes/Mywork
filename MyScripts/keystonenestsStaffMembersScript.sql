

CREATE TABLE public.staffmembers
(
    id integer NOT NULL,
    userid integer NOT NULL,
    roleid integer NOT NULL,
    employed bit varying(1) NOT NULL,
    CONSTRAINT staffmembers_pkey PRIMARY KEY (id),
    CONSTRAINT fk_roleid FOREIGN KEY (roleid)
        REFERENCES public.role (id),
    CONSTRAINT fk_staffuserid FOREIGN KEY (userid)
        REFERENCES public.users (id) 
)
