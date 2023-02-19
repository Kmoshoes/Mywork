

CREATE TABLE public.contactdetails
(
    id integer NOT NULL,
    userid integer NOT NULL,
    phonenumber character varying(120) NOT NULL,
    email character varying(120) NOT NULL,
    CONSTRAINT contactdetails_pkey PRIMARY KEY (id),
    CONSTRAINT fk_usercontact FOREIGN KEY (userid)
        REFERENCES public.users (id)
)

