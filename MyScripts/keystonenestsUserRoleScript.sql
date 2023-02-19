
CREATE TABLE public.userrole
(
    id integer NOT NULL ,
    userid integer NOT NULL,
    roleid integer NOT NULL,
    CONSTRAINT userrole_pkey PRIMARY KEY (id),
    CONSTRAINT fk_roleuserid FOREIGN KEY (userid)
        REFERENCES public.users (id),
    CONSTRAINT fk_userroleid FOREIGN KEY (roleid)
        REFERENCES public.role (id) 
)
