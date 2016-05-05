create or replace 
procedure    GetCountries (p_region_id NUMBER, p_cursor out SYS_REFCURSOR)
as
begin
  open p_cursor for select country_id, country_name, region_id from HR.countries where region_id = p_region_id;
end;


DECLARE
  P_REGION_ID NUMBER;
  P_CURSOR SYS_REFCURSOR;
BEGIN
  P_REGION_ID := 2;

  HR.GETCOUNTRIES(
    P_REGION_ID => P_REGION_ID,
    P_CURSOR => P_CURSOR
  );
  /* Legacy output: 
DBMS_OUTPUT.PUT_LINE('P_CURSOR = ' || P_CURSOR);
*/ 
  :P_CURSOR := P_CURSOR; --<-- Cursor
END;
