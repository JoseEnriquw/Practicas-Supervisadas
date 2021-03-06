PGDMP         #            
    y            TP_Practica_Supervisada    13.4    13.4 )    ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    16394    TP_Practica_Supervisada    DATABASE     y   CREATE DATABASE "TP_Practica_Supervisada" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Argentina.1252';
 )   DROP DATABASE "TP_Practica_Supervisada";
                postgres    false            ?            1255    24595    SP_TR_EliminarPais()    FUNCTION     ?   CREATE FUNCTION public."SP_TR_EliminarPais"() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin 

delete from provincias where idpais=old.id;

return old;

end
$$;
 -   DROP FUNCTION public."SP_TR_EliminarPais"();
       public          postgres    false            ?            1255    24593    SP_TR_EliminarPartidos()    FUNCTION     ?   CREATE FUNCTION public."SP_TR_EliminarPartidos"() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin

delete from localidades where idpartido=old.id;

RETURN old;
end
$$;
 1   DROP FUNCTION public."SP_TR_EliminarPartidos"();
       public          postgres    false            ?            1255    24590    SP_TR_EliminarProvincia()    FUNCTION     ?   CREATE FUNCTION public."SP_TR_EliminarProvincia"() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN

delete from partidos where idprovincia=old.id;
	
	return old;
END
$$;
 2   DROP FUNCTION public."SP_TR_EliminarProvincia"();
       public          postgres    false            ?            1259    24727    localidades    TABLE     u   CREATE TABLE public.localidades (
    id bigint NOT NULL,
    nombre text NOT NULL,
    idpartido bigint NOT NULL
);
    DROP TABLE public.localidades;
       public         heap    postgres    false            ?            1259    24725    localidades_id_seq    SEQUENCE     {   CREATE SEQUENCE public.localidades_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.localidades_id_seq;
       public          postgres    false    207            ?           0    0    localidades_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.localidades_id_seq OWNED BY public.localidades.id;
          public          postgres    false    206            ?            1259    24675    pais    TABLE     P   CREATE TABLE public.pais (
    id integer NOT NULL,
    nombre text NOT NULL
);
    DROP TABLE public.pais;
       public         heap    postgres    false            ?            1259    24673    pais_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.pais_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.pais_id_seq;
       public          postgres    false    201            ?           0    0    pais_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.pais_id_seq OWNED BY public.pais.id;
          public          postgres    false    200            ?            1259    24710    partidos    TABLE     u   CREATE TABLE public.partidos (
    id bigint NOT NULL,
    nombre text NOT NULL,
    idprovincia integer NOT NULL
);
    DROP TABLE public.partidos;
       public         heap    postgres    false            ?            1259    24708    partidos_id_seq    SEQUENCE     x   CREATE SEQUENCE public.partidos_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.partidos_id_seq;
       public          postgres    false    205            ?           0    0    partidos_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.partidos_id_seq OWNED BY public.partidos.id;
          public          postgres    false    204            ?            1259    24693 
   provincias    TABLE     s   CREATE TABLE public.provincias (
    id integer NOT NULL,
    nombre text NOT NULL,
    idpais integer NOT NULL
);
    DROP TABLE public.provincias;
       public         heap    postgres    false            ?            1259    24691    provincias_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.provincias_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.provincias_id_seq;
       public          postgres    false    203            ?           0    0    provincias_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.provincias_id_seq OWNED BY public.provincias.id;
          public          postgres    false    202            >           2604    24730    localidades id    DEFAULT     p   ALTER TABLE ONLY public.localidades ALTER COLUMN id SET DEFAULT nextval('public.localidades_id_seq'::regclass);
 =   ALTER TABLE public.localidades ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    207    206    207            ;           2604    24678    pais id    DEFAULT     b   ALTER TABLE ONLY public.pais ALTER COLUMN id SET DEFAULT nextval('public.pais_id_seq'::regclass);
 6   ALTER TABLE public.pais ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    201    200    201            =           2604    24713    partidos id    DEFAULT     j   ALTER TABLE ONLY public.partidos ALTER COLUMN id SET DEFAULT nextval('public.partidos_id_seq'::regclass);
 :   ALTER TABLE public.partidos ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    204    205    205            <           2604    24696    provincias id    DEFAULT     n   ALTER TABLE ONLY public.provincias ALTER COLUMN id SET DEFAULT nextval('public.provincias_id_seq'::regclass);
 <   ALTER TABLE public.provincias ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    202    203    203            ?          0    24727    localidades 
   TABLE DATA           <   COPY public.localidades (id, nombre, idpartido) FROM stdin;
    public          postgres    false    207   4-       ?          0    24675    pais 
   TABLE DATA           *   COPY public.pais (id, nombre) FROM stdin;
    public          postgres    false    201   Q-       ?          0    24710    partidos 
   TABLE DATA           ;   COPY public.partidos (id, nombre, idprovincia) FROM stdin;
    public          postgres    false    205   n-       ?          0    24693 
   provincias 
   TABLE DATA           8   COPY public.provincias (id, nombre, idpais) FROM stdin;
    public          postgres    false    203   ?-       ?           0    0    localidades_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.localidades_id_seq', 1, false);
          public          postgres    false    206            ?           0    0    pais_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.pais_id_seq', 1, false);
          public          postgres    false    200            ?           0    0    partidos_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.partidos_id_seq', 1, false);
          public          postgres    false    204            ?           0    0    provincias_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.provincias_id_seq', 1, false);
          public          postgres    false    202            F           2606    24735    localidades localidades_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.localidades
    ADD CONSTRAINT localidades_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.localidades DROP CONSTRAINT localidades_pkey;
       public            postgres    false    207            @           2606    24683    pais pais_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.pais
    ADD CONSTRAINT pais_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.pais DROP CONSTRAINT pais_pkey;
       public            postgres    false    201            D           2606    24718    partidos partidos_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.partidos
    ADD CONSTRAINT partidos_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.partidos DROP CONSTRAINT partidos_pkey;
       public            postgres    false    205            B           2606    24701    provincias provincias_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.provincias
    ADD CONSTRAINT provincias_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.provincias DROP CONSTRAINT provincias_pkey;
       public            postgres    false    203            J           2620    24684    pais TR_EliminarPais    TRIGGER     {   CREATE TRIGGER "TR_EliminarPais" BEFORE DELETE ON public.pais FOR EACH ROW EXECUTE FUNCTION public."SP_TR_EliminarPais"();
 /   DROP TRIGGER "TR_EliminarPais" ON public.pais;
       public          postgres    false    209    201            L           2620    24724    partidos TR_EliminarPartido    TRIGGER     ?   CREATE TRIGGER "TR_EliminarPartido" BEFORE DELETE ON public.partidos FOR EACH ROW EXECUTE FUNCTION public."SP_TR_EliminarPartidos"();
 6   DROP TRIGGER "TR_EliminarPartido" ON public.partidos;
       public          postgres    false    205    208            K           2620    24707    provincias TR_EliminarProvincia    TRIGGER     ?   CREATE TRIGGER "TR_EliminarProvincia" BEFORE DELETE ON public.provincias FOR EACH ROW EXECUTE FUNCTION public."SP_TR_EliminarProvincia"();
 :   DROP TRIGGER "TR_EliminarProvincia" ON public.provincias;
       public          postgres    false    210    203            I           2606    24736 &   localidades localidades_idpartido_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.localidades
    ADD CONSTRAINT localidades_idpartido_fkey FOREIGN KEY (idpartido) REFERENCES public.partidos(id);
 P   ALTER TABLE ONLY public.localidades DROP CONSTRAINT localidades_idpartido_fkey;
       public          postgres    false    205    2884    207            H           2606    24719 "   partidos partidos_idprovincia_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.partidos
    ADD CONSTRAINT partidos_idprovincia_fkey FOREIGN KEY (idprovincia) REFERENCES public.provincias(id);
 L   ALTER TABLE ONLY public.partidos DROP CONSTRAINT partidos_idprovincia_fkey;
       public          postgres    false    203    2882    205            G           2606    24702 !   provincias provincias_idpais_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.provincias
    ADD CONSTRAINT provincias_idpais_fkey FOREIGN KEY (idpais) REFERENCES public.pais(id);
 K   ALTER TABLE ONLY public.provincias DROP CONSTRAINT provincias_idpais_fkey;
       public          postgres    false    203    2880    201            ?      x?????? ? ?      ?      x?????? ? ?      ?      x?????? ? ?      ?      x?????? ? ?     