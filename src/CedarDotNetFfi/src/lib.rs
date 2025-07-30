use std::ffi::{c_char, CString, CStr};

use std::str::FromStr;

use cedar_policy::{
    Policy,
    PolicySet
};

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
    get_sdk_version as internal_get_sdk_version,
    policy_set_text_to_parts as internal_policy_set_text_to_parts
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

// -- Utilities --

#[unsafe(no_mangle)]
pub fn policy_format_text_to_json(text: *const c_char) -> *const c_char {
    let text_str = unsafe { CStr::from_ptr(text).to_str().unwrap() };

    let policy = Policy::parse(None, text_str).expect("Could not parse policy text");

    let value = policy.to_json().expect("Could not serialise response");

    CString::new(value.to_string()).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub fn policy_format_json_to_text(json: *const c_char) -> *const c_char {
    let json_str = unsafe { CStr::from_ptr(json).to_str().unwrap() };

    let value = serde_json::from_str(json_str).expect("Could not parse policy JSON");

    let policy = Policy::from_json(None, value).expect("Could not create policy from JSON");

    CString::new(policy.to_string()).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub fn load_policy_set(text: *const c_char) -> *const c_char {    
    let text_str = unsafe { CStr::from_ptr(text).to_str().unwrap() };

    let policy_set = PolicySet::from_str(text_str).expect("Could not load policy set");

    let arr = policy_set
        .policies()
        .map(|p| p.to_string())
        .collect::<Vec<_>>();

    let result_json_str = serde_json::to_string(&arr).unwrap();
    
    CString::new(result_json_str.to_string()).unwrap().into_raw()
}

#[unsafe(no_mangle)]
pub fn policy_set_text_to_parts(text: *const c_char) -> *const c_char {
    let text_str = unsafe { CStr::from_ptr(text).to_str().unwrap() };

    let parts = internal_policy_set_text_to_parts(text_str);

    let result_json_str = serde_json::to_string(&parts).expect("Could not serialize parts to JSON");

    CString::new(result_json_str).unwrap().into_raw()
}