
CREATE TABLE IF NOT EXISTS public.visitors
(
    id SERIAL PRIMARY KEY,
    userid integer NOT NULL,
	tenantid integer NOT NULL,
    dateofvisit date NOT NULL,
    timein time without time zone NOT NULL,
    timeout time without time zone,
    CONSTRAINT fk_visitoruserid FOREIGN KEY (userid) REFERENCES public.users,
	CONSTRAINT fk_visitortenantid FOREIGN KEY (id) REFERENCES public.tenants
)

