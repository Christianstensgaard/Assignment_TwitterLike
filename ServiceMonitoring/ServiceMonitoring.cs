using System.Text;

namespace ServiceMonitoring
{
  public class ServiceMonitoring
    {
        private Dictionary<string, string> _registeredServices = new Dictionary<string, string>();

        public void RegisterService(string serviceKey, string publicKey)
        {
            _registeredServices[serviceKey] = publicKey;
            Console.WriteLine($"Service {serviceKey} registered with public key {publicKey}.");
        }

        public bool InvokeService(string serviceKey, string payload)
        {
            Console.WriteLine($"Validating service with key: {serviceKey}...");

            if (_registeredServices.ContainsKey(serviceKey))
            {
                // Simulate payload decryption
                string decryptedPayload = DecryptPayload(payload, _registeredServices[serviceKey]);
                Console.WriteLine($"Payload decrypted: {decryptedPayload}");

                // Forward to the service
                ForwardToService(serviceKey, decryptedPayload);
                return true;
            }
            else
            {
                // Security measure
                ActivateSecurityMeasures(serviceKey);
                return false;
            }
        }

        private void ForwardToService(string serviceKey, string payload)
        {
            Console.WriteLine($"Forwarding request to {serviceKey} with payload: {payload}...");
        }

        private void ActivateSecurityMeasures(string serviceKey)
        {
            Console.WriteLine($"Security Alert! Unauthorized service key: {serviceKey}");
        }

        private string DecryptPayload(string payload, string publicKey)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(payload))));
        }
    }
}
