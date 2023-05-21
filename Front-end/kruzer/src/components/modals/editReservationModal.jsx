import { Select, Modal, Form, Input, notification } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";

function EditReservationModal({
  visible,
  setVisible,
  reservation = {},
  setRefetch,
  krstarenje = {},
}) {
  const { id, brojputnika } = reservation;
  const handleCancel = () => {
    setVisible(false);
  };

  console.log("reservation to be editet ", reservation);

  async function handleOk() {
    setVisible(false);
    const data = form.getFieldsValue();
    console.log("data ", data);
    const response = api
      .put("/api/Rezervacija/" + id, data)
      .then((res) => {
        notification.open({
          message: "Rezervacija ažurirana!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });
    form.resetFields();
    /*if (response.status == 204) {
      notification.open({
        message: "Rezervacija ažurirana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
    */
  }

  const [form] = Form.useForm();

  const validateBrojPutnika = (_, value) => {
    if (value && value <= 0) {
      return Promise.reject(new Error("Broj putnika mora biti barem 1"));
    }
    if (
      value &&
      value - brojputnika + krstarenje.popunjenost <= krstarenje.kapacitet
    ) {
      return Promise.reject(new Error("Broj putnika premašuje kapacitet"));
    }
    return Promise.resolve();
  };

  return (
    <>
      <Modal
        title="Uredi rezervaciju"
        open={visible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Pošalji"
        cancelText="Natrag"
      >
        <Form
          form={form}
          layout="vertical"
          autoComplete="off"
          name="validateOnly"
        >
          <Form.Item
            name="brojputnika"
            label="Broj putnika"
            rules={[
              {
                required: true,
                validator: validateBrojPutnika,
              },
            ]}
            initialValue={brojputnika}
          >
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default EditReservationModal;
