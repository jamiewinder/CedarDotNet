use std::ffi::{c_char, CString, CStr};

use cedar_policy::ffi::{
    check_parse_policy_set_json_str,
    check_parse_schema_json_str,
    check_parse_entities_json_str,
    check_parse_context_json_str,
    format_json_str,
    is_authorized_json_str,
    is_authorized_partial_json_str,
    validate_json_str,
    get_lang_version as internal_get_lang_version,
    get_sdk_version as internal_get_sdk_version
};

#[unsafe(no_mangle)]
pub unsafe extern "C" fn free_string(ptr: *mut c_char) {
    if !ptr.is_null() {
        unsafe {
            drop(CString::from_raw(ptr))
        };
    }
}

#[unsafe(no_mangle)]
pub extern "C" fn check_parse_policy_set(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = check_parse_policy_set_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn check_parse_schema(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = check_parse_schema_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn check_parse_entities(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = check_parse_entities_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn check_parse_context(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = check_parse_context_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn format(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = format_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn is_authorized(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = is_authorized_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn is_authorized_partial(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = is_authorized_partial_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn validate(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let result_str = validate_json_str(json_str).unwrap();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn get_lang_version() -> *const c_char {
    let result_str = internal_get_lang_version();

    CString::new(result_str).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub extern "C" fn get_sdk_version() -> *const c_char {
    let result_str = internal_get_sdk_version();

    CString::new(result_str).unwrap().into_raw()
}
